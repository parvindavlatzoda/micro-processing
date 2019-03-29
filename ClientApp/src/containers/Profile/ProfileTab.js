import React, { Component, Fragment } from 'react'
import { Tabs } from 'antd'
import { Row, Col } from 'antd'
import ProfileInfo from './ProfileInfo'

const TabPane = Tabs.TabPane

class ProfileTab extends Component {
  render() {
    return(
    <Fragment>
      <Row>
        <Col span={24}> 
          <Tabs defaultActiveKey="1">
            <TabPane tab="Tab 1" key="1">Tab 1
              <ProfileInfo/>
            </TabPane>
            <TabPane tab="Tab 2" key="2">Tab 2</TabPane>
            <TabPane tab="Tab 3" key="3">Tab 3</TabPane>
          </Tabs>
        </Col>
      </Row> 
     
    </Fragment>
    )   
  }
}

export default ProfileTab