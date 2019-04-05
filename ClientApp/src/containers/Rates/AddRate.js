import React, {Component, Fragment} from 'react'
import { Modal, Button, Select, Input } from 'antd';
import Auth from '../../modules/Auth'
import axios from 'axios'

const Option = Select.Option

function handleChange(value) {
  console.log(`selected ${value}`);
}

class AddRate extends Component {




  state = { 
    visible: false,
    currencies: []

  }

  showModal = () => {
    this.setState({
      visible: true,
    });
  }
  componentDidMount = () => {
    axios.get('/api/1.0/keeper/currencies')
    .then(function (response) {
      const currencies = response.data
      console.log(currencies)
      this.setState(currencies)

   
  })
    .catch(function (error) {
      console.log(error);
  });
  }

    

  handleOk = (e) => {
    console.log(e);
    this.setState({
      visible: false,
    });
  }

  handleCancel = (e) => {
    console.log(e);
    this.setState({
      visible: false,
    });
  }

  render() {
    const {currencies} = this.state
    console.log('DATA', currencies)
    return (
      <Fragment>
        <Button type="primary" style={{margin: '1em'}} onClick={this.showModal}>
           Добавить Курс
        </Button>
        <Modal
          title=" ADD RATE"
          centered
          visible={this.state.visible}
          onOk={this.handleOk}
          onCancel={this.handleCancel}
        >
         <Select defaultValue="1" onChange={handleChange} style={{ margin: '1em' }}>
          {!!currencies &&
          currencies.map((cur, index) => <Option value={index}>{cur.title}</Option>)}
         </Select>
          <Input type="number" placeholder="курс" style={{margin: '1em'}}></Input>
        </Modal>
      </Fragment>
    );
  }
}

export default AddRate