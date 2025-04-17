import BannerSlide from '@/components/Home/BannerSlide'
import EventCategoriesSection from '@/components/Home/EventCategoriesSection'
import HotEventsCarousel from '@/components/Home/HotEventsCarousel'
import SuggestSection from '@/components/Home/SuggestSection'
import UpcomingEventsSection from '@/components/Home/UpcomingEventsSection'
import { useGetEvents } from '@/hooks'
import React, { useState } from 'react'
import { Header, Page } from 'zmp-ui'

const HomePage = () => {
  const [isAdmin, setIsAdmin] = useState(false)
  return (
    <Page
      className="py-[60px] space-y-4"
    >
      <Header title='Trang chủ'></Header>
      <BannerSlide></BannerSlide>
      <SuggestSection></SuggestSection>
      <UpcomingEventsSection></UpcomingEventsSection>
      <HotEventsCarousel></HotEventsCarousel>
      <EventCategoriesSection></EventCategoriesSection>
      
    </Page>
  )
}

export default HomePage