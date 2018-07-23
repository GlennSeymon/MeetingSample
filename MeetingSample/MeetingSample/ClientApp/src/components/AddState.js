import React, { Component } from 'react';
import { RouteComponentProps } from 'react-router';

interface AddStateDataState {
    title: string;
    loading: boolean;
	stateCode: number;
	descShort: string;
	descLong: string;
}

var stateCodeParam = 0;

export class AddState extends Component<RouteComponentProps<{}>, AddStateDataState> {
    constructor(props) {
		super(props);

		stateCodeParam = this.props.match.params.statecode; 

		const stateInitial = {
			title: "", loading: true, stateCode: stateCodeParam, descShort: "", descLong: ""
		}

		this.state = stateInitial;

		if (stateCodeParam > 0) {
			fetch('api/States/' + stateCodeParam)
				.then(response => response.json())
				.then(data => {
					this.setState({ title: "Edit", loading: false, stateCode: data.statecode, descShort: data.descShort, descLong: data.descLong });
				});
		}
		else
			this.state = { title: "Create", loading: false, stateCode: 0, descShort: "", descLong: "" };

        this.handleSave = this.handleSave.bind(this);
		this.handleCancel = this.handleCancel.bind(this);
		this.handleChange = this.handleChange.bind(this);
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

	handleChange(event) {
		const target = event.target;
		const value = target.type === 'checkbox' ? target.checked : target.value;
		const name = target.name;

		this.setState({
			[name]: value
		});
	}

	handleSave(event) {
		event.preventDefault();

		const data = {
			"stateCode": stateCodeParam,
			"descShort": this.state.descShort,
			"descLong": this.state.descLong
		}

		if (stateCodeParam) {
			fetch('api/States/' + stateCodeParam, {
				method: 'PUT',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(data)
            }).then((responseJson) => {
                    this.props.history.push("/fetchstate");
                })
        }
        else {
            fetch('api/States/', {
				method: 'POST',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(data)
            }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchstate");
                })
        }
    }

    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchstate");
    }

    renderCreateForm() {
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
					<input type="hidden" name="stateCode" value={stateCodeParam} />
                </div>
                <div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="descShort">Desc Short</label>
                    <div className="col-md-4">
						<input className="form-control" type="text" name="descShort" defaultValue={this.state.descShort} onChange={this.handleChange} required />
                    </div>
                </div >
                <div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="descLong">Desc Long</label>
                    <div className="col-md-4">
						<input className="form-control" type="text" name="descLong" defaultValue={this.state.descLong} onChange={this.handleChange} required />
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