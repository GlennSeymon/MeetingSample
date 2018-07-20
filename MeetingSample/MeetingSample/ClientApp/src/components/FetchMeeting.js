import React, { Component } from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import Moment from 'react-moment';

export class MeetingData {
	meetCode: number = 0;
	meetDate: Date;
	title: string = "";
	stateDescLong: number = 0;
	venueName: number = 0;
}    

interface FetchMeetingDataState {
    meetingList: MeetingData[];
    loading: boolean;
}

export class FetchMeeting extends Component<RouteComponentProps<{}>, FetchMeetingDataState> {
    constructor(props) {
        super(props);
        this.state = { meetingList: [], loading: true };

        fetch('api/Meetings')
            .then(response => response.json())
            .then(data => {
                this.setState({ meetingList: data, loading: false });
            });

        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
    }

    // Handle Delete request for a meeting  
    handleDelete(id: number) {
        if (!window.confirm("Do you want to delete meeting with code: " + id))
            return;
		else {
            fetch('api/Meetings/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
					{
                        meetingList: this.state.meetingList.filter((rec) => {
                            return rec.meetCode !== id;
                        })
                    });
            });
        }
    }

	handleEdit(id: number) {
		this.props.history.push("/meeting/edit/" + id);
    }

    // Returns the HTML table to the render() method.  
    renderMeetingTable(meetingList: MeetingData[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Meeting Code</th>
                    <th>Meeting Date</th>
					<th>Title</th>
					<th>State</th>
					<th>Venue</th>
                    <th>Action</th>
                </tr>
			</thead>
            <tbody>
                {meetingList.map(meeting =>
                    <tr key={meeting.meetCode}>
                        <td>{meeting.meetCode}</td>
						<td><Moment format="DD/MM/YYYY">{meeting.meetDate}</Moment></td>
						<td>{meeting.title}</td>
						<td>{meeting.stateDescLong}</td>
						<td>{meeting.venueName}</td>
                        <td>
                            <a className="action" onClick={(id) => this.handleEdit(meeting.meetCode)}>Edit</a>  |
                            <a className="action" onClick={(id) => this.handleDelete(meeting.meetCode)}>Delete</a>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderMeetingTable(this.state.meetingList);

        return <div>
            <h1>Meeting List</h1>
            <p>
                <Link to="/addmeeting">Create New</Link>
            </p>
            {contents}
        </div>;
    }
}

FetchMeeting.displayName = 'FetchMeeting';