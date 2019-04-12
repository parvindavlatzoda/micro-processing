import React from 'react'
import {
  Form, DatePicker, TimePicker, Button, Input, Select, message
} from 'antd';
import styled from 'styled-components'
import download from 'downloadjs'

const { MonthPicker, RangePicker } = DatePicker;
const Option = Select.Option;


class TimeRelatedForm extends React.Component {

  state = {
    services: [],
    selected: 'all',    
    from: new Date(), 
    to: new Date(),
  }
  

  handleChange = selected => {
    this.setState({ selected })
  }

  handleSubmit = e => {
    e.preventDefault()

  this.props.form.validateFields((err, fieldsValue) => {
    if (err) {
      return
    }
    const rangeTimeValue = fieldsValue['range-time-picker']
    const from = rangeTimeValue[0].format('YYYY.MM.DD')
    const to = rangeTimeValue[1].format('YYYY.MM.DD')

    // тут будем делать запрос
    message.loading('Генерация отчета', 2)
    
    const  { selected: serviceId } = this.state
    console.log('ServId', serviceId)

    fetch(`/api/1.0/keeper/reports/csv?${serviceId !== 'all' ? `serviceId=${serviceId}&` : ''}from=${from}&to=${to}`, {
      })
        .then(res => res.blob()).then(blob => download(blob, 'report', 'text/csv'))
        .then(message.success('Отчет сгенерирован', 2))
        .then(message.info('Дождитесь окончания загрузки файла', 2.5))
        .catch((err) => {
          console.log(err);
        }); 
  })
}

  render() {
    const { selected } = this.state
    const { getFieldDecorator } = this.props.form;
    const formItemLayout = {
      labelCol: {
        xs: { span: 12 },
        sm: { span: 3 },
      },
      wrapperCol: {
        xs: { span: 12 },
        sm: { span: 8 },
      },
    };

    const config = {
      rules: [{ type: 'object', required: false, message: 'Please select time!' }],
    };
    const rangeConfig = {
      rules: [{ type: 'array', required: false, message: 'Please select time!' }],
    };
    return (
      <Form {...formItemLayout} onSubmit={this.handleSubmit}>
      
        <Form.Item
          label="Период"
          style={{ margin: '0' }}
        >
          {getFieldDecorator('range-time-picker', rangeConfig)(
            <RangePicker onChange={this.handleChangeRange} showTime format="YYYY-MM-DD HH:mm:ss" />
          )}
        </Form.Item>
      
        <Form.Item
          label="Service"
          style={{ margin: '0' }}
        >
          <Select
            showSearch
            style={{ width: 200 }}
            placeholder="Выберите услугу"
            defaultValue="all"
            optionFilterProp="children"

            filterOption={(input, option) => option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
            onChange={this.handleChange}
          >
            <Option value="all">Все</Option>
            <Option value="324">Билайн РТ (МБТ, рубль)</Option>
            <Option value="326">Вавилон-М (МБТ, рубль)</Option>
            <Option value="323">Мегафон РТ (МБТ, рубль)</Option>
            <Option value="325">Тселл (МБТ, рубль)</Option>
          </Select>
        </Form.Item>

        <Form.Item
          style={{ margin: '0' }}
          wrapperCol={{
            xs: { span: 24, offset: 0 },
            sm: { span: 16, offset: 3 },
          }}
        >
          <Button icon="download"   htmlType="submit">Download .csv</Button>
        </Form.Item>
      </Form>
    );
  }
}

const WrappedTimeRelatedForm = Form.create({ name: 'time_related_controls' })(TimeRelatedForm);

export default WrappedTimeRelatedForm