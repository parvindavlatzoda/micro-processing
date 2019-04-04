import React, { Component, Fragment } from 'react'
import { Button , Table} from 'antd'

const data = [{
    key: '1',
    curency: 'RUB',
    age: 32,
    address: 'New York No. 1 Lake Park',
  }, {
    key: '2',
    curency: 'EUR',
    age: 42,
    address: 'London No. 1 Lake Park',
  }, {
    key: '3',
    curency: 'USD',
    age: 32,
    address: 'Sidney No. 1 Lake Park',
  }, {
    key: '4',
    curency: 'RUB',
    age: 32,
    address: 'London No. 2 Lake Park',
  }];
  

class Rates extends Component {
  

    state = {
        filteredInfo: null,
        sortedInfo: null,
    };

    handleChange = (pagination, filters, sorter) => {
        console.log('Various parameters', pagination, filters, sorter);
        this.setState({
        filteredInfo: filters,
        sortedInfo: sorter,
        });
    }

    clearFilters = () => {
        this.setState({ filteredInfo: null });
    }

    clearAll = () => {
    this.setState({
        filteredInfo: null,
        sortedInfo: null,
    });
    }

    setAgeSort = () => {
        this.setState({
        sortedInfo: {
            order: 'descend',
            columnKey: 'age',
        },
        });
    } 

    render() {
        let { sortedInfo, filteredInfo } = this.state;
        sortedInfo = sortedInfo || {};
        filteredInfo = filteredInfo || {};
        const columns = [{
          title: 'Валюта',
          dataIndex: 'curency',
          key: 'curency',
          filters: [
            { text: 'RUB', value: 'RUB' },
            { text: 'USD', value: 'USD' },
          ],
          filteredValue: filteredInfo.curency || null,
          onFilter: (value, record) => record.curency.includes(value),
          sorter: (a, b) => a.curency.length - b.curency.length,
          sortOrder: sortedInfo.columnKey === 'curency' && sortedInfo.order,
        }, {
            title: 'Address',
            dataIndex: 'address',
            key: 'address',
                     
          },  {
            title: 'Age',
            dataIndex: 'age',
            key: 'age',
            sorter: (a, b) => a.age - b.age,
            sortOrder: sortedInfo.columnKey === 'age' && sortedInfo.order,
        } ];



    return (
      <Fragment> 
        <Table columns={columns} dataSource={data} onChange={this.handleChange} />
      </Fragment>
    )
  }
}

export default Rates