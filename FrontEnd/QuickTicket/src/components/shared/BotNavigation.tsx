import { HomeIcon } from '@/static/icon/HomeIcon';
import ProfileIcon from '@/static/icon/ProfileIcon';
import TicketIcon from '@/static/icon/TicketIcon';
import React from 'react'
import { BottomNavigation, Icon, Page } from "zmp-ui";

const BotNavigation = ({onChange, activeKey}) => {
  return (
    <BottomNavigation
        fixed
        activeKey={activeKey}
        onChange={onChange}
        className='!text-[14px]'
    >
      {/* home */}
      <BottomNavigation.Item
          key="home"
          label="Trang chủ"
          icon={<HomeIcon></HomeIcon>}
          linkTo=''
        />

        {/* ticket */}
        <BottomNavigation.Item
          key="ticket"
          label="Vé của tôi"
          icon={<TicketIcon></TicketIcon>}
          linkTo=''
        />

        {/* profile  */}
        <BottomNavigation.Item
          key="profile"
          label="Tài Khoản"
          icon={<ProfileIcon></ProfileIcon>}
          linkTo=''
        />
    </BottomNavigation>
  )
}

export default BotNavigation