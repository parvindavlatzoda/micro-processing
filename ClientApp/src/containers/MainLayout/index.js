import React, { Component } from 'react'
import styled from 'styled-components'

import { Link } from 'react-router-dom'

import {
    Layout, Menu, Breadcrumb, Icon,
    Row, Avatar, Dropdown, PageHeader
} from 'antd'

const menu = (
    <Menu>
      <Menu.Item key="0">
        <a href="#">Profile</a>
      </Menu.Item>
      <Menu.Item key="1">
        <a href="#">Settings</a>
      </Menu.Item>
      <Menu.Divider />
      <Menu.Item key="3">Sign Out</Menu.Item>
    </Menu>
  );



const SubMenu = Menu.SubMenu;
const MenuItemGroup = Menu.ItemGroup;

const {
    Header: LayoutHeader,
    Content,
    Footer,
    Sider,
} = Layout

const Header = styled(LayoutHeader) `
  background-color: #ffffff;
  height: initial;
`

const Logo = styled.h1 `
  text-align: center;
  text-justify: center;
  font-size: 19px;
  margin-top: 1em;
  margin-bottom: 1em;
  overflow: hidden;
  color: #ffffff;
  font-weight: 900;
`



// import NavMenu from './NavMenu'
// import Breadcrumbs from 'containers/Breadcrumbs'

class MainLayout extends Component {

    state = {
        collapsed: true,
    }

    handleCollapse = collapsed => {
        this.setState({ collapsed, } )
    }

    render() {

        const {
            collapsed,
        } = this.state

        return (
            <Layout style={{ minHeight: '100vh' }}>
                <Sider
                    collapsible
                    collapsed={collapsed}
                    onCollapse={this.handleCollapse}
                    theme="dark"
                >
                    <Logo>
                        MP
                    </Logo>
                    <Menu theme="dark" defaultSelectedKeys={['1']} mode="inline" theme="dark">
                        <Menu.Item key="1">
                            <Icon type="dashboard" />
                            <span>Dashboard</span>
                            <Link to="/" />
                        </Menu.Item>
                        <Menu.Item key="2">
                            <Icon type="dollar" />
                            <span>Очередь платежей</span>
                            <Link to="/cashboxes" />
                        </Menu.Item>
                       
                        {/* <SubMenu
              key="sub1"
              title={<span><Icon type="user" /><span>User</span></span>}
            >
              <Menu.Item key="3">Клиенты</Menu.Item>
              <Menu.Item key="4">Билеты</Menu.Item>
              <Menu.Item key="5">Отчеты</Menu.Item>
            </SubMenu> */}
                        {/* <SubMenu
              key="sub2"
              title={<span><Icon type="team" /><span>Team</span></span>}
            >
              <Menu.Item key="6">Team 1</Menu.Item>
              <Menu.Item key="8">Team 2</Menu.Item>
            </SubMenu> */}
                        {/* <Menu.Item key="9">
              <Icon type="file" />
              <span>File</span>
            </Menu.Item> */}
                    </Menu>
                </Sider>
                <Layout>

                    <Header>
                        <Row type="flex" justify="space-between" >
                            <PageHeader
                                title="Поиск платежей"
                                subTitle="На этой странице можно повеситься"
                            />
                            <Dropdown overlay={menu} placement="bottomCenter" trigger={['click']} >
                                <Avatar icon="user" style={{ color: '#f56a00', backgroundColor: '#fde3cf', marginTop: '0.7em'}} />
                            </Dropdown>
                        </Row>
                    </Header>

                    <Content style={{ margin: '0 16px' }}>
                        <Breadcrumb style={{ margin: '16px 0' }}>
                            <Breadcrumb.Item>User</Breadcrumb.Item>
                            <Breadcrumb.Item>Tim</Breadcrumb.Item>
                        </Breadcrumb>
                        <div style={{ padding: 24, background: '#fff', minHeight: 360 }}>
                            {this.props.children}
                        </div>
                    </Content>
                    <Footer style={{ textAlign: 'center' }}>
                        {`Micro Processin ©${new Date().getFullYear()} Created by Meduse Software`}
                    </Footer>
                </Layout>


                {/* <header className='header'> */}
                {/* <NavMenu /> */}
                {/* </header> */}
                {/* <main className='content'> */}
                {/* <nav className='breadcrumb'> */}
                {/* <Breadcrumbs/> */}
                {/* </nav> */}

                {/* </main> */}
            </Layout>
        )
    }

}

export default MainLayout