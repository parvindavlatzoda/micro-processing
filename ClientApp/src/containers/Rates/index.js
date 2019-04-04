import React, { Component, Fragment } from 'react'
import { Button, Table, Row, Col } from 'antd'
import TableRates from './TableRates'
import AddRate from './AddRate';


class Rates extends Component {
  render() {
    return (
      <Fragment> 
        <Row>
          <Col span='15'>
            <TableRates/>           
          </Col>
          <Col>
            <AddRate/> 
            <Button type="primary" style={{margin: '1em'}} >Добавить Валюту</Button>  
                     
          </Col>
        </Row>
      </Fragment>
    )
  }
}

export default Rates