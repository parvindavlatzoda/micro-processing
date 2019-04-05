import React, {Component, Fragment} from 'react'
import { Modal, Button, Select, Input } from 'antd';
import Auth from '../../modules/Auth'
import axios from 'axios'

const Option = Select.Option

class AddRate extends Component {

  state = { 
    visible: false,
    currencies: [],
    selected: null,
    rate: null,
    state : null,
    isLoading: false,
  }

  showModal = () => {
    this.setState({
      visible: true,
    });
  }
  componentDidMount = () => {
    axios
      .get('/api/1.0/keeper/currencies')
      .then(response => {
        const currencies = response.data.currencies
        console.log(currencies)
        this.setState({ currencies })
      })
      .catch(function(error) {
        console.log(error)
      })
  }

  handleChange = selected => {
    this.setState({ selected })
  }
  
  handleChangeInput = event => {
    const rate = event.target.value
    this.setState({ rate })
  }   

  handleAddCurrency = () => {
    const { selected, currencies, rate } = this.state
    const currencyId = currencies[selected].id
    this.setState({ isLoading: true })
    axios({
      method: 'post',
      url: 'api/1.0/keeper/rates',

      data: {
        currencyId,
        rate,
      },
    })
    .then(res => {
      this.setState({ isLoading: true, visible: false, })
    })
      .catch(err => console.log(err))
  }

  handleCancel = (e) => {
    console.log(e);
    this.setState({
      visible: false,
    });
  }

  render() {
    const {currencies, selected, rate, isLoading } = this.state
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
          footer={[
            <Button key="back" onClick={this.handleCancel}>
              Отмена
            </Button>,
            <Button
              key="submit"
              type="primary"
              loading={isLoading}
              onClick={this.handleAddCurrency}
              disabled={selected == null}
            >
              Добавить
            </Button>,
          ]}
        
        >
        {selected}
        <Select onChange={this.handleChange}  style={{ margin: '1em', minWidth: '10em', }}>
            {!!currencies &&
              currencies.map((cur, index) => (
                <Option key={cur.id} value={index} >
                  {cur.title}
                </Option>
              ))}
          </Select>
          <Input type="number" placeholder="курс" onChange={this.handleChangeInput} style={{margin: '1em'}}></Input>
          
        </Modal>
      </Fragment>
    );
  }
}

export default AddRate