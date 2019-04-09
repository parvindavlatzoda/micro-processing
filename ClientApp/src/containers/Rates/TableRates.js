import React, { Component, Fragment } from 'react'
import { Button , Table} from 'antd'
import Auth from '../../modules/Auth'
import pgFormatDate from '../../utils/pgFormatDate'



function onChange(pagination, filters, sorter) {
  console.log('params', pagination, filters, sorter);
}


class TableRates extends Component {
  
  state = {
    filteredInfo: null,
    sortedInfo: null,
    isLoading: true,
    transactions: [],
  };

  componentDidMount = () => {
    this.getRates()
  }
  
  getRates = () => {
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
        
      })
      .catch((err) => {
        console.log(err);
      });
  }

 
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