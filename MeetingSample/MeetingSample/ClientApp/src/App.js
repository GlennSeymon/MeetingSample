import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchState } from './components/FetchState';
import { AddState } from './components/AddState'; 

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
		<Route path='/fetchstate' component={FetchState} />
		<Route path='/addstate' component={AddState} />
		<Route path='/state/edit/:statecode' component={AddState} />
      </Layout>
    );
  }
}