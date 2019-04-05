import React, { Component, Fragment } from 'react'
import { Button , Table} from 'antd'
import Auth from '../../modules/Auth'
import pgFormatDate from '../../utils/pgFormatDate'



function onChange(pagination, filters, sorter) {
  console.log('params', pagination, filters, sorter);
}

const data = [{
    key: '1',
    curency: 'RUB',
    age: 32,
    createdAt: 'New York No. 1 Lake Park',
  }, {
    key: '2',
    curency: 'EUR',
    age: 42,
    createdAt: 'London No. 1 Lake Park',
  }, {
    key: '3',
    curency: 'USD',
    age: 32,
    createdAt: 'Sidney No. 1 Lake Park',
  }, {
    key: '4',
    curency: 'RUB',
    age: 32,
    address: 'London No. 2 Lake Park',
  }];
  

class TableRates extends Component {
  
  constructor(props) {
    super(props)

    this.state = {
      isLoading: true,
      transactions: [],
    }

    fetch('/api/1.0/keeper/rates?pageSize=500', {
      headers: {
        'Authorization': `bearer ${Auth.getToken()}`,
        'Content-type': 'application/json'
      }
    })
      .then(res => res.json())
      .then(data => {
        this.setState({
          transactions: data,
          isLoading: false
        });
        //console.log(data)
      })
      .catch((err) => {
        console.log(err);
      });
  }


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
          dataIndex: 'isoCode',
          key: 'isoCode',
          filters: [
            { text: 'RUB', value: 'RUB' },
            { text: 'USD', value: 'USD' },
          ],
          filteredValue: filteredInfo.isoCode || null,
          onFilter: (value, record) => record.isoCode.includes(value),
          sorter: (a, b) => a.isoCode.length - b.isoCode.length,
          sortOrder: sortedInfo.columnKey === 'isoCode' && sortedInfo.order,
        }, {
            title: 'курс',
            dataIndex: 'rate',
            key: 'rate',
                     
          },  {
            title: 'createdAt',
            dataIndex: 'createdAt',
            key: 'createdAt',
            sorter: (a, b) => a.createdAt - b.createdAt,
            sortOrder: sortedInfo.columnKey === 'createdAt' && sortedInfo.order,
           
            onFilter: (value, record) => record.createdAt.indexOf(value) === 0,
            sorter: (a, b) => a.createdAt.length - b.createdAt.length,
            //sortDirections: ['descend'],
            render: text => <a href="javascript:;">{pgFormatDate(text)}</a>,
            
        } ];



    return (
      <Fragment> 
        <Table columns={columns} dataSource={this.state.transactions} onChange={onChange} size="small"/>

      </Fragment>
    )
  }
}

export default TableRates