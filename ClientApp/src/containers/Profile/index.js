import React, { Component, Fragment } from 'react'
import { Button } from 'antd'
import ProfileInfoBar from './ProfileInfoBar'
import ProfileTab from './ProfileTab'

class Profile extends Component {
  render() {
    return (
      <Fragment>
        <ProfileInfoBar/>
        <ProfileTab/> 
        <div>Purgen Ivanych</div>
        <Button type="primary">click me</Button>
        
      </Fragment>
    )
  }
}

export default Profile