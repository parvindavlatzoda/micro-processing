import React from 'react'
import {
  Form, DatePicker, TimePicker, Button, Input, Select, message
} from 'antd';
import styled from 'styled-components'

const { MonthPicker, RangePicker } = DatePicker;
const Option = Select.Option;

const success = () => {
  message.loading('Генерация отчета', 2.5)
    .then(() => message.success('Отчет сгенерирован', 1.5))
    .then(() => message.info('Дождитесь окончания загрузки файла', 2.5));
};

class TimeRelatedForm extends React.Component {
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
          label="Интервал"
          style={{ margin: '0' }}
        >
          <Select
            showSearch
            style={{ width: 200 }}
            placeholder="Выберите интервал"
            defaultValue="jack"
            optionFilterProp="children"
            // onChange={handleChange}
            // onFocus={handleFocus}
            // onBlur={handleBlur}
            filterOption={(input, option) => option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
          >
            <Option value="jack">За последний час</Option>
            <Option value="lucy">Lucy</Option>
            <Option value="tom">Tom</Option>
          </Select>
        </Form.Item>
        <Form.Item
          label="Период"
          style={{ margin: '0' }}
        >
          {getFieldDecorator('range-time-picker', rangeConfig)(
            <RangePicker showTime format="YYYY-MM-DD HH:mm:ss" />
          )}
        </Form.Item>
        <Form.Item
          label="Аккаунт"
          style={{ margin: '0' }}
        >
            <Input placeholder="93 588 11 01" />
        </Form.Item>
        <Form.Item
          label="ID транзакции"
          style={{ margin: '0' }}
        >
          <Input placeholder="9d22e8c4-56a9-409d-a9bf-82ca666f7761" />
        </Form.Item>
        <Form.Item
          label="Шлюз"
          style={{ margin: '0' }}
        >
          <Select
            showSearch
            style={{ width: 200 }}
            placeholder="Выберите статус"
            defaultValue="all"
            optionFilterProp="children"
            // onChange={handleChange}
            // onFocus={handleFocus}
            // onBlur={handleBlur}
            filterOption={(input, option) => option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
          >
            <Option value="all">Все</Option>
            <Option value="lucy">Lucy</Option>
            <Option value="tom">Tom</Option>
          </Select> 
        </Form.Item>
        <Form.Item
          label="Service"
          style={{ margin: '0' }}
        >
          <Select
            showSearch
            style={{ width: 200 }}
            placeholder="Выберите статус"
            defaultValue="all"
            optionFilterProp="children"
            // onChange={handleChange}
            // onFocus={handleFocus}
            // onBlur={handleBlur}
            filterOption={(input, option) => option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
          >
            <Option value="all">Все</Option>
            <Option value="lucy">Lucy</Option>
            <Option value="tom">Tom</Option>
          </Select>
        </Form.Item>
        <Form.Item
          label="Status"
          style={{ margin: '0' }}
        >
          <Select
            showSearch
            style={{ width: 200 }}
            placeholder="Выберите статус"
            defaultValue="all"
            optionFilterProp="children"
            // onChange={handleChange}
            // onFocus={handleFocus}
            // onBlur={handleBlur}
            filterOption={(input, option) => option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
          >
            <Option value="all">Все</Option>
            <Option value="lucy">Lucy</Option>
            <Option value="tom">Tom</Option>
          </Select>
        </Form.Item>
        <Form.Item
          style={{ margin: '0' }}
          wrapperCol={{
            xs: { span: 24, offset: 0 },
            sm: { span: 16, offset: 3 },
          }}
        >
          <Button type="primary" htmlType="submit" style={{ marginRight: '1em' }}>Search</Button>
          <Button icon="download" onClick={success} htmlType="submit">Download .csv</Button>

        </Form.Item>
      </Form>
    );
  }
}

const WrappedTimeRelatedForm = Form.create({ name: 'time_related_controls' })(TimeRelatedForm);

export default WrappedTimeRelatedForm