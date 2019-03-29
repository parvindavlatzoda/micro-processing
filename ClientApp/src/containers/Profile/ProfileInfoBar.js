import React, {Component, Fragment} from 'react'
import {Menu, Icon} from 'antd'
import { Row, Col } from 'antd'

class ProfileInfoBar extends Component {
  render() {
    return (
      <Fragment>

        <Row>
          <Col span={9}>
              <h1>your Info</h1>
          </Col>
          <Col span={5}>
            Go passwordless  
          </Col>
          <Col span={5}>
          Change your password
          </Col>
          <Col span={5}>
          Manage your adress
          </Col>
        </Row>
    
     </Fragment> 
     
  )
  }
}




export default ProfileInfoBar