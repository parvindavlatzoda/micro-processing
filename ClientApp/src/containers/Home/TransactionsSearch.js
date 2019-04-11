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
  serviceId: null,
  from: new Date(),
  to: new Date(),
}

 success = () => {
  message.loading('Генерация отчета', 2)
  
  const  { serviceId, from, to, selected } = this.state

  fetch(`/api/1.0/keeper/reports/csv?serviceId=${serviceId}&from=${from}&to=${to}`, {
    })
      .then(res => res.blob()).then(blob => download(blob, 'report', 'text/csv'))
      .then(message.success('Отчет сгенерирован', 2))
      .then(message.info('Дождитесь окончания загрузки файла', 2.5))
      .catch((err) => {
        console.log(err);
      });
  
};

  handleChange = selected => {
    this.setState({ selected })
  }

  handleChangeInput = event => {
    const rate = event.target.value
    this.setState({ rate })
  }   

  handleSubmit = (e) => {
    e.preventDefault();

    this.props.form.validateFields((err, fieldsValue) => {
      if (err) {
        return;
      }

      // Should format date value before submit.
      const rangeValue = fieldsValue['range-picker'];
      const rangeTimeValue = fieldsValue['range-time-picker'];
      const values = {
        ...fieldsValue,
        'date-picker': fieldsValue['date-picker'].format('YYYY-MM-DD'),
        'date-time-picker': fieldsValue['date-time-picker'].format('YYYY-MM-DD HH:mm:ss'),
        'month-picker': fieldsValue['month-picker'].format('YYYY-MM'),
        'range-picker': [rangeValue[0].format('YYYY-MM-DD'), rangeValue[1].format('YYYY-MM-DD')],
        'range-time-picker': [
          rangeTimeValue[0].format('YYYY-MM-DD HH:mm:ss'),
          rangeTimeValue[1].format('YYYY-MM-DD HH:mm:ss'),
        ],
        'time-picker': fieldsValue['time-picker'].format('HH:mm:ss'),
      };
      console.log('Received values of form: ', values);
    });
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
            <RangePicker onChange={this.handleChangeInput} showTime format="YYYY-MM-DD HH:mm:ss" />
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
          <Button icon="download" onClick={this.success} htmlType="button">Download .csv</Button>
        </Form.Item>
      </Form>
    );
  }
}

const WrappedTimeRelatedForm = Form.create({ name: 'time_related_controls' })(TimeRelatedForm);

export default WrappedTimeRelatedForm