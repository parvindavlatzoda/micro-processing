import React, { Fragment } from 'react';

export default class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true
        };

    }

    render() {
        return (
            <Fragment>
                <h1>Welcome to Home</h1>
            </Fragment>
        );
    }
}