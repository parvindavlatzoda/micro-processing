import React, { Component } from 'react'
import { Switch } from 'react-router-dom'
import PrivateRoute from './routes/PrivateRoute'
import LoggedOutRoute from './routes/LoggedOutRoute'

import MainLayout from './containers/MainLayout'

import Home from './containers/Home'
import Profile from './containers/Profile'
import SignIn from './containers/SignIn'

class App extends Component {
  render() {
    return (
      <Switch>
        <LoggedOutRoute exact path='/signin' component={SignIn} />
        <MainLayout>
            <PrivateRoute exact path='/' component={Home} />
            <PrivateRoute exact path='/profile' component={Profile} />
        </MainLayout>

      </Switch>
    )
  }
}

export default App