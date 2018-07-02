import React, { Component } from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { StateData } from './FetchState'; 

interface AddStateDataState {
    title: string;
    loading: boolean;
    stateData: StateData;
} 

export class AddState extends Component<RouteComponentProps<{}>, AddStateDataState> {
    constructor(props) {
        super(props);
		this.state = { title: "", loading: true, stateData: new StateData };

		var stateCode = this.props.match.params["stateCode"]; 

		// This will set state for Edit employee  
		if (stateCode > 0) {
			fetch('api/State/' + stateCode)
				//.then(response => response.json() as Promise<StateData>)
				.then(response => response.json())
				.then(data => {
					this.setState({ title: "Edit", loading: false, stateData: data });
				});
		}

		// This will set state for Add employee  
		else {
			this.state = { title: "Create", loading: false, cityList: [], stateData: new StateData };
		}  

        // This binding is necessary to make "this" work in the callback  
        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm();

		return <div>
			<h1>{this.state.descLong}</h1>
            <h3>State</h3>
            <hr />
            {contents}
        </div>;
    }

    // This will handle the submit form event.  
    handleSave(event) {
        event.preventDefault();
        const data = new FormData(event.target);

        // PUT request for Edit state.  
        if (this.state.stateData.stateCode) {
            fetch('api/State/', {
                method: 'PUT',
                body: data,

            }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchstate");
                })
        }

        // POST request for Add state.  
        else {
            fetch('api/State/', {
                method: 'POST',
                body: data,

            }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchstate");
                })
        }
    }

    // This will handle Cancel button click event.  
    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchstate");
    }

    // Returns the HTML Form to the render() method.  
    renderCreateForm() {
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
                    <input type="hidden" name="stateCode" value={this.state.stateData.stateCode} />
                </div>
                <div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="DescShort">Desc Short</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="DescShort" defaultValue={this.state.stateData.descShort} required />
                    </div>
                </div >
                <div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="DescLong">Desc Long</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="DescLong" defaultValue={this.state.stateData.descLong} required />
                    </div>
                </div >
                <div className="form-group">
                    <button type="submit" className="btn btn-default">Save</button>
                    <button className="btn" onClick={this.handleCancel}>Cancel</button>
                </div >
            </form >
        )
    }
}  