import React, {Component, Fragment} from 'react'
import {Menu, Icon} from 'antd'
import { Row, Col } from 'antd'

class ProfileInfoBar extends Component {
  render() {
    return (
      <Fragment>

     <Row>
       <Col span={9}>col-6</Col>
       <Col span={5}>col-6</Col>
       <Col span={5}>col-6</Col>
       <Col span={5}>col-6</Col>
     </Row>
    
     </Fragment> 
     
  )
  }
}




export default ProfileInfoBar