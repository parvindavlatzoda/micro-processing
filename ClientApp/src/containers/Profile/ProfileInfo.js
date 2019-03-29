import React, { Component, Fragment } from 'react'
import { Row, Col } from 'antd'
import user from './user.jpeg'

class InfoProfile extends Component {
  render() {
    return(
      <Fragment>
        <Row>
          
          <Col span={6} offset={2}>
            <Row>
              <img src={user} style={{ borderRadius: "100%"}} width='200'/>
            </Row>
            <Row>
              change picture
            </Row>
          </Col>
          
          <Col span={16}>

           <Row>
            <h2> %username%</h2>
            edit name
           </Row> 

          <Col span={8}>
            <Row>
              @email
            </Row>
            <Row>
              birth date
            </Row>
            <Row>
              region
            </Row>
          </Col>

          <Col span={8}>
            <Row>
              manage 
            </Row>
            <Row>
              edit date of birth
            </Row>
            <Row>
              edit country/region
            </Row>
          </Col>
          

          </Col>

        </Row>
      </Fragment>  
    )
  }  
}

export default InfoProfile