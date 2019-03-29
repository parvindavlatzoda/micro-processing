import React, { Component, Fragment } from 'react'
import { Button, Row } from 'antd'


class Gateway extends Component {
  render() {
    return (
      <Fragment>
        <Row >
         <Button type="primary">Create</Button>
         <Button type="primary">Edit</Button>
         <Button type="primary">Delete</Button>
        </Row>  
      </Fragment>
    )
  }
}

export default Gateway