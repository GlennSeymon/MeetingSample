import React, { Component } from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';

interface FetchStateDataState {
    //stateList: StateData[];
    loading: boolean;
}

export class FetchState extends Component<RouteComponentProps<{}>, FetchStateDataState> {
    constructor(props) {
        super(props);
        this.state = { stateList: [], loading: true };

        //fetch('api/State/Index')
        fetch('api/States')
            //.then(response => response.json() as Promise<StateData[]>)
            .then(response => response.json())
            .then(data => {
                this.setState({ stateList: data, loading: false });
            });

        // This binding is necessary to make "this" work in the callback  
        //this.handleDelete = this.handleDelete.bind(this);
        //this.handleEdit = this.handleEdit.bind(this);
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderStateTable(this.state.stateList);

        return <div>
            <h1>State Data</h1>
            <p>This component demonstrates fetching State data from the server.</p>
            <p>
                <Link to="/addstate">Create New</Link>
            </p>
            {contents}
        </div>;
    }
/*
    // Handle Delete request for an state  
    handleDelete(id: number) {
        if (!confirm("Do you want to delete state with Id: " + id))
            return;
        else {
            fetch('api/State/Delete/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
                    {
                        stateList: this.state.stateList.filter((rec) => {
                            return (rec.stateId != id);
                        })
                    });
            });
        }
    }

    handleEdit(id: number) {
        this.props.history.push("/state/edit/" + id);
    }
*/
    // Returns the HTML table to the render() method.  
    renderStateTable(stateList: StateData[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th></th>
                    <th>State Code</th>
                    <th>Desc Short</th>
                    <th>Desc Long</th>
                </tr>
            </thead>
            <tbody>
                {stateList.map(state =>
                    <tr key={state.stateId}>
                        <td></td>
                        <td>{state.stateCode}</td>
                        <td>{state.descShort}</td>
                        <td>{state.descLong}</td>
                        <td>
                            <a className="action" onClick={(id) => this.handleEdit(state.stateId)}>Edit</a>  |
                            <a className="action" onClick={(id) => this.handleDelete(state.stateId)}>Delete</a>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;
    }
}

export class StateData {
    stateCode: number = 0;
    descShort: string = "";
    descLong: string = "";
}    