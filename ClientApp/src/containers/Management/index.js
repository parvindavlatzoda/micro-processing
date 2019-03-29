import React, { Component, Fragment } from 'react'
import { Tabs } from 'antd'
import Gateway from './Gateway'

const TabPane = Tabs.TabPane

class Management extends Component {
  render() {
    return (
      <Fragment>
        <h1>Hey, Manager</h1>
        <Tabs defaultActiveKey="1" tabPosition={"left"} >
          <TabPane tab="Пользователи" key="1">Content of tab 1</TabPane>
          <TabPane tab="Шлюзы" key="2"><Gateway/></TabPane>
          <TabPane tab="Продукты" key="3">Content of tab 3</TabPane>
          <TabPane tab="Валюты" key="4">Content of tab 4</TabPane>
        </Tabs>
      </Fragment>
    )
  }
}

export default Management