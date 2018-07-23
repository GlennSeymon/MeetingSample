import React, { Component } from 'react';
import { RouteComponentProps } from 'react-router';
import { Label } from '@atlaskit/field-base';
import Select from '@atlaskit/select';
import FieldText, { FieldTextStateless } from '@atlaskit/field-text';

interface AddVenueDataState {
    title: string;
    loading: boolean;
	venueCode: number;
	venueTitle: string;
	stateCode: number;
	venueCode: number;
	states: any;
	statesLoaded: boolean;
}

var venueCodeParam = 0;

export class AddVenue extends Component<RouteComponentProps<{}>, AddVenueDataState> {
    constructor(props) {
		super(props);

		venueCodeParam = this.props.match.params.venuecode; 

		const stateInitial = {
			title: "", loading: true, venueCode: venueCodeParam, venueTitle: "", stateCode: 0, statesLoaded: false
		}

		this.state = stateInitial;

		if (venueCodeParam > 0) {
			fetch('api/Venues/' + venueCodeParam)
				.then(response => response.json())
				.then(data => {
					this.setState({ title: "Edit", loading: false, venueCode: data.venuecode, venueTitle: data.name, stateCode: data.stateCode });
				});
		}
		else
			this.state = { title: "Create", loading: false, venueCode: 0, venueTitle: "", stateCode: 0 };

        this.handleSave = this.handleSave.bind(this);
		this.handleCancel = this.handleCancel.bind(this);
		this.handleChange = this.handleChange.bind(this);
		this.handleChangeState = this.handleChangeState.bind(this);
	}

	componentDidMount() {
		this.fetchStates();
	}

	fetchStates() {
		fetch('api/States')
			.then((res) => {
				return res.json();
			}).then((states) => {
				let stateArr = [];
				for (let state of states)
					stateArr.push({ "value": state.stateCode, "label": state.descLong });

				this.setState({ states: stateArr, statesLoaded: true });
			});
	}

	render() {
		let contents = (this.state.loading || !this.state.statesLoaded ) ? <p><em>Loading...</em></p> : this.renderCreateForm();

		return <div>
			<h1>{this.state.title}</h1>
			<h3>Venue</h3>
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

	handleChangeState(event) {
		this.setState({ stateCode: event.value});
	}

	handleSave(event) {
		event.preventDefault();

		const data = {
			"VenueCode": venueCodeParam,
			"Name": this.state.venueTitle,
			"StateCode": this.state.stateCode
		}

		if (venueCodeParam) {
			fetch('api/Venues/' + venueCodeParam, {
				method: 'PUT',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(data)
            }).then((responseJson) => {
                    this.props.history.push("/fetchvenue");
                })
        }
        else {
            fetch('api/Venues/', {
				method: 'POST',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(data)
            }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchvenue");
                })
        }
    }

    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchvenue");
    }

	renderCreateForm() {
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
					<input type="hidden" name="venueCode" value={venueCodeParam} />
                </div>
                <div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="venueTitle">Venue Title</label>
					<div className="col-md-4">
						<input className="form-control" type="text" name="venueTitle" value={this.state.venueTitle} onChange={this.handleChange} required />
                    </div>
				</div >
				<div className="form-group row" >
					<label className=" control-label col-md-12" htmlFor="stateCode">State</label>
					<div className="col-md-4">
						<Select className="single-select" classNamePrefix="react-select" name="stateCode" options={this.state.states} defaultValue={this.state.states[this.state.stateCode - 1]} onChange={this.handleChangeState} placeholder="Choose a State" />
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