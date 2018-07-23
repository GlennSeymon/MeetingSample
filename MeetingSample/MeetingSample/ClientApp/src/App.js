import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchState } from './components/FetchState';
import { AddState } from './components/AddState'; 
import { FetchMeeting } from './components/FetchMeeting';
import { AddMeeting } from './components/AddMeeting'; 
import { FetchVenue } from './components/FetchVenue';
import { AddVenue } from './components/AddVenue'; 

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
		<Route path='/fetchstate' component={FetchState} />
		<Route path='/addstate' component={AddState} />
		<Route path='/state/edit/:statecode' component={AddState} />
		<Route path='/fetchvenue' component={FetchVenue} />
		<Route path='/addvenue' component={AddVenue} />
		<Route path='/venue/edit/:venuecode' component={AddVenue} />
      </Layout>
    );
  }
}