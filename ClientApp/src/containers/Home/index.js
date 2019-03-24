import React, { Fragment } from 'react'
import TransactionsSearch from './TransactionsSearch'

import { Popconfirm, Icon, Divider, Table, Tag, Button } from 'antd';

const ButtonGroup = Button.Group;

const columns = [{
  title: 'Created at',
  dataIndex: 'name',
  filters: [{
    text: 'Joe',
    value: 'Joe',
  }, {
    text: 'Jim',
    value: 'Jim',
  }, {
    text: 'Submenu',
    value: 'Submenu',
    children: [{
      text: 'Green',
      value: 'Green',
    }, {
      text: 'Black',
      value: 'Black',
    }],
  }],
  // specify the condition of filtering result
  // here is that finding the name started with `value`
  onFilter: (value, record) => record.name.indexOf(value) === 0,
  sorter: (a, b) => a.name.length - b.name.length,
  sortDirections: ['descend'],
}, {
  title: 'ID',
  dataIndex: 'age',
  defaultSortOrder: 'descend',
}, {
  title: 'Incoming ID',
  dataIndex: 'age',
  defaultSortOrder: 'descend',
}, {
  title: 'Service',
  dataIndex: 'age',
  defaultSortOrder: 'descend',
  sorter: (a, b) => a.age - b.age,
}, {
  title: 'Account',
  dataIndex: 'age',
  defaultSortOrder: 'descend',
}, {
  title: 'Amount',
  dataIndex: 'age',
  defaultSortOrder: 'descend',
  sorter: (a, b) => a.age - b.age,
}, {
  title: 'Status',
  dataIndex: 'status',
  render: tags => (
    <span>
      {tags.map(tag => {
        let color = tag == "accepted" ? 'geekblue' : 'green';
        if (tag === 'canceled') {
          color = 'volcano';
        }
        return <Tag color={color} key={tag}>{tag.toUpperCase()}</Tag>;
      })}
    </span>
  ),
}, {
  title: 'Gate',
  dataIndex: 'address',
  filters: [{
    text: 'London',
    value: 'London',
  }, {
    text: 'New York',
    value: 'New York',
  }],
  filterMultiple: false,
  onFilter: (value, record) => record.address.indexOf(value) === 0,
  sorter: (a, b) => a.address.length - b.address.length,
  sortDirections: ['descend', 'ascend'],
}, {
  title: 'Action',
  key: 'action',
  render: (text, record) => (
    <span>
      <a href="javascript:;">Details</a>
      <Divider type="vertical" />
      <Popconfirm title="Are you sure？" icon={<Icon type="question-circle-o" style={{ color: 'red' }} />}>
        <a href="#">Delete</a>
      </Popconfirm>
    </span>
  ),
}];

const data = [{
  key: '1',
  name: 'John Brown',
  age: 32,
  address: 'New York No. 1 Lake Park',
  status: ['accepted'],
}, {
  key: '2',
  name: 'Jim Green',
  age: 42,
  address: 'London No. 1 Lake Park',
  status: ['success'],
}, {
  key: '3',
  name: 'Joe Black',
  age: 32,
  address: 'Sidney No. 1 Lake Park',
  status: ['success'],
}, {
  key: '4',
  name: 'Jim Red',
  age: 32,
  address: 'London No. 2 Lake Park',
  status: ['canceled'],
}];

function onChange(pagination, filters, sorter) {
  console.log('params', pagination, filters, sorter);
}



export default class Home extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      loading: true
    };

  }

  render() {
    return (
      <Fragment>
        <TransactionsSearch/>
        <br/>
        <Table columns={columns} dataSource={data} onChange={onChange} size="small"/>
      </Fragment>
    );
  }
}