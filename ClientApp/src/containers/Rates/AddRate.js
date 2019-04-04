import React, {Component, Fragment} from 'react'
import { Modal, Button, Select, Input } from 'antd';
import Auth from '../../modules/Auth'

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
    axios.get('/route')
    .then(function (response) {
    console.log(response);
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
          <Select defaultValue="2" onChange={handleChange} style={{margin: '1em'}}>
            <Option value="1">Россиский рубль</Option>
            <Option value="2">Американский доллар</Option>
          </Select>
          <Input type="number" placeholder="курс" style={{margin: '1em'}}></Input>
        </Modal>
      </Fragment>
    );
  }
}

export default AddRate