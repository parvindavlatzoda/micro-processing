import React from 'react'
import { Route, Redirect } from 'react-router-dom'

import Auth from '../modules/Auth';

const PrivateRoute = ({ component: Component, ...rest }) => (
    <Route {...rest} render={props => (
        Auth.isUserAuthenticated() ? (
            <Component {...props} {...rest} />
        ) : (
            <Redirect to={{
                pathname: '/signin',
                state: { from: props.location }
            }} />
        )
    )} />
)

export default PrivateRoute