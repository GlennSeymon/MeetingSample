import React, { Component } from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';

export class StateData {
    stateCode: number = 0;
    descShort: string = "";
    descLong: string = "";
}    

interface FetchStateDataState {
    stateList: StateData[];
    loading: boolean;
}

export class FetchState extends Component<RouteComponentProps<{}>, FetchStateDataState> {
    constructor(props) {
        super(props);
        this.state = { stateList: [], loading: true };

        fetch('api/States')
            .then(response => response.json())
            .then(data => {
                this.setState({ stateList: data, loading: false });
            });

        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
    }

    // Handle Delete request for an state  
    handleDelete(id: number) {
        if (!window.confirm("Do you want to delete state with code: " + id))
            return;
		else {
            fetch('api/States/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
					{
                        stateList: this.state.stateList.filter((rec) => {
                            return rec.stateCode !== id;
                        })
                    });
            });
        }
    }

	handleEdit(id: number) {
		this.props.history.push("/state/edit/" + id);
    }

    // Returns the HTML table to the render() method.  
    renderStateTable(stateList: StateData[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>State Code</th>
                    <th>Desc Short</th>
                    <th>Desc Long</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                {stateList.map(state =>
                    <tr key={state.stateCode}>
                        <td>{state.stateCode}</td>
                        <td>{state.descShort}</td>
                        <td>{state.descLong}</td>
                        <td>
                            <a className="action" onClick={(id) => this.handleEdit(state.stateCode)}>Edit</a>  |
                            <a className="action" onClick={(id) => this.handleDelete(state.stateCode)}>Delete</a>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderStateTable(this.state.stateList);

        return <div>
            <h1>State List</h1>
            <p>
                <Link to="/addstate">Create New</Link>
            </p>
            {contents}
        </div>;
    }
}
FetchState.displayName = 'FetchState';