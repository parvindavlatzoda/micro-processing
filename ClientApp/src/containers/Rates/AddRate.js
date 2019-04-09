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
    defaultId: null,
  }

  componentDidMount = () => { 
    this.getCurrencies()   
  }

  getCurrencies =  () => {
    axios
      .get('/api/1.0/keeper/currencies')
      .then(response => {
        const currencies = response.data.currencies
        console.log(currencies)
        const defaultId = response.data.default.id
        console.log(defaultId)
        this.setState({ currencies, defaultId })   
       
      })
      .catch(function(error) {
        console.log(error)
      })

  }

  showModal = () => {
    this.setState({
      visible: true,
    });
  }

  handleChange = selected => {
    this.setState({ selected })
  }
  
  handleChangeInput = event => {
    const rate = event.target.value
    this.setState({ rate })
  }   

  handleAddCurrency = () => {
    const { selected, currencies, rate, defaultId } = this.state
    const currencyId = selected ? selected : defaultId
    console.log(rate, currencyId)
    this.setState({ isLoading: true })
    axios({
      method: 'post',
      url: 'api/1.0/keeper/rates',

      data: {
        currencyId,
        rate,
      },
    })
    .then(response => {
      this.setState({ isLoading: false, visible: false, })
      this.getCurrencies()
    })

      .catch(err => { 
        this.setState({ isLoading: false })
        console.log( err )
      })
  }

  handleCancel = (e) => {
    console.log(e);
    this.setState({
      visible: false,
    });
  }

  render() {
    const {currencies, selected, rate, isLoading , defaultId} = this.state
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
              disabled={
                (selected == null && defaultId == null) ||
                (rate == null || rate === '')
              }
            >
              Добавить
            </Button>,
          ]}
        
        >
        
        <Select onChange={this.handleChange}   style={{ margin: '1em' }} value={ selected ? selected : defaultId }>
            {!!currencies &&
              currencies.map((cur, index) => (
                <Option key={cur.id} value={cur.id} >
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