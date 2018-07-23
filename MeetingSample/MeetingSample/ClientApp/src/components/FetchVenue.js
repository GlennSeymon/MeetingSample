import React, { Component } from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';

export class VenueData {
	venueCode: number = 0;
	name: string = "";
	stateDescLong: string = 0;
}    

interface FetchVenueDataState {
    venueList: VenueData[];
    loading: boolean;
}

export class FetchVenue extends Component<RouteComponentProps<{}>, FetchVenueDataState> {
    constructor(props) {
        super(props);
        this.state = { venueList: [], loading: true };

        fetch('api/Venues')
            .then(response => response.json())
            .then(data => {
                this.setState({ venueList: data, loading: false });
            });

        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
    }

    // Handle Delete request for a venue  
    handleDelete(id: number) {
        if (!window.confirm("Do you want to delete venue with code: " + id))
            return;
		else {
            fetch('api/Venues/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
					{
                        venueList: this.state.venueList.filter((rec) => {
                            return rec.venueCode !== id;
                        })
                    });
            });
        }
    }

	handleEdit(id: number) {
		this.props.history.push("/venue/edit/" + id);
    }

    // Returns the HTML table to the render() method.  
    renderVenueTable(venueList: VenueData[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Venue Code</th>
					<th>Name</th>
					<th>State</th>
                    <th>Action</th>
                </tr>
			</thead>
            <tbody>
                {venueList.map(venue =>
					<tr key={venue.venueCode}>
						<td>{venue.venueCode}</td>
						<td>{venue.name}</td>
						<td>{venue.stateDescLong}</td>
                        <td>
							<a className="action" onClick={(id) => this.handleEdit(venue.venueCode)}>Edit</a>  |
                            <a className="action" onClick={(id) => this.handleDelete(venue.venueCode)}>Delete</a>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    render() {
		let contents = this.state.loading ? <p><em>Loading...</em></p> : this.renderVenueTable(this.state.venueList);

        return <div>
			<h1>Venue List</h1>
            <p>
				<Link to="/addvenue">Create New</Link>
            </p>
            {contents}
        </div>;
    }
}

FetchVenue.displayName = 'FetchVenue';