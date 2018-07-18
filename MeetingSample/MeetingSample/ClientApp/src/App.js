import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchState } from './components/FetchState';
import { AddState } from './components/AddState'; 
import { FetchMeeting } from './components/FetchMeeting';
import { AddMeeting } from './components/AddMeeting'; 

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
		<Route path='/fetchstate' component={FetchState} />
		<Route path='/addstate' component={AddState} />
		<Route path='/state/edit/:statecode' component={AddState} />
		<Route path='/fetchmeeting' component={FetchMeeting} />
		<Route path='/addmeeting' component={AddMeeting} />
		<Route path='/meeting/edit/:meetcode' component={AddMeeting} />
      </Layout>
    );
  }
}