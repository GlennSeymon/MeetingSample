import React, { Component } from 'react';
import { RouteComponentProps } from 'react-router';
import { Label } from '@atlaskit/field-base';
import { DatePicker } from '@atlaskit/datetime-picker';
import Select from '@atlaskit/select';

interface AddMeetingDataState {
    title: string;
    loading: boolean;
	meetCode: number;
	meetDate: string;
	meetTitle: string;
	stateCode: number;
	venueCode: number;
	states: any;
	venues: any;
}

var meetingCodeParam = 0;

export class AddMeeting extends Component<RouteComponentProps<{}>, AddMeetingDataState> {
    constructor(props) {
		super(props);

		meetingCodeParam = this.props.match.params.meetcode; 

		const stateInitial = {
			title: "", loading: true, meetCode: meetingCodeParam, meetDate: "", meetTitle: "", stateCode: 0, venueCode: 0
		}

		this.state = stateInitial;

		if (meetingCodeParam > 0) {
			fetch('api/Meetings/' + meetingCodeParam)
				.then(response => response.json())
				.then(data => {
					this.setState({ title: "Edit", loading: false, meetCode: data.meetcode, meetDate: data.meetDate, meetTitle: data.title, stateCode: data.stateCode, venueCode: data.venueCode });
				});
		}
		else
			this.state = { title: "Create", loading: false, meetCode: 0, meetDate: "", meetTitle: "", stateCode: 0, venueCode: 0 };

        this.handleSave = this.handleSave.bind(this);
		this.handleCancel = this.handleCancel.bind(this);
		this.handleChange = this.handleChange.bind(this);
		this.handleChangeDatePicker = this.handleChangeDatePicker.bind(this);
	}

	componentDidMount() {
		this.fetchStates();
		this.fetchVenues();
	}

	fetchStates() {
		fetch('api/States')
			.then((res) => {
				return res.json();
			}).then((states) => {
				let stateArr = [];
				for (let state of states)
					stateArr.push({ "value": state.stateCode, "label": state.descLong });

				this.setState({ states: stateArr })
			});
	}

	fetchVenues() {
		fetch('api/Venues')
			.then((res) => {
				return res.json();
			}).then((venues) => {
				let venueArr = [];
				for (let venue of venues)
					venueArr.push({ "value": venue.venueCode, "label": venue.name });

				this.setState({ venues: venueArr })
			});
	}

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm();

		return <div>
			<h1>{this.state.title}</h1>
            <h3>Meeting</h3>
            <hr />
            {contents}
        </div>;
	}

	handleChangeDatePicker = (value: string) => {
		this.recentlySelected = true;
		this.setState(
			{
				meetDate: value,
				isOpen: false,
			},
			() => {
				setTimeout(() => {
					this.recentlySelected = false;
				}, 200);
			},
		);
	};

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
			"meetCode": meetingCodeParam,
			"meetDate": this.state.meetDate,
			"meetTitle": this.state.title,
			"stateCode": this.state.stateCode,
			"venueCode": this.state.venueCode
		}

		if (meetingCodeParam) {
			fetch('api/Meetings/' + meetingCodeParam, {
				method: 'PUT',
				body: data
            }).then((responseJson) => {
                    this.props.history.push("/fetchmeeting");
                })
        }
        else {
            fetch('api/Meetings/', {
				method: 'POST',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(data)
            }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchmeeting");
                })
        }
    }

    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchmeeting");
    }

	renderCreateForm() {
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
					<input type="hidden" name="meetCode" value={meetingCodeParam} />
                </div>
                <div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="meetDate">Meeting Date</label>
					<div className="col-md-4">
						<DatePicker id="meetDate" value={this.state.meetDate} onChange={this.handleChangeDatePicker} />
                    </div>
                </div >
                <div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="title">Meeting Title</label>
					<div className="col-md-4">
						<input className="form-control" type="text" name="title" value={this.state.meetTitle} onChange={this.handleChange} required />
                    </div>
				</div >
				<div className="form-group row" >
					<label className=" control-label col-md-12" htmlFor="stateCode">State</label>
					<div className="col-md-4">
						<Select className="single-select" classNamePrefix="react-select" name="stateCode" defaultValue={this.state.stateCode} options={this.state.states} placeholder="Choose a State" />
					</div>
				</div >
				<div className="form-group row" >
					<label className=" control-label col-md-12" htmlFor="venueCode">Venue</label>
					<div className="col-md-4">
						<Select className="single-select" classNamePrefix="react-select" name="venueCode" defaultValue={this.state.venueCode} options={this.state.venues} placeholder="Choose a Venue" />
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
