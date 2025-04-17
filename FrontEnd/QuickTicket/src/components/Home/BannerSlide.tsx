import React from 'react'
import { useGetEvents } from '@/hooks'
import { Swiper } from 'zmp-ui'

const BannerSlide = () => {
    const {events, loading, error} = useGetEvents()
    console.log(events)
    return (
        <Swiper duration={3000} autoplay  className='rounded-none'>
            {events.map((event)=>
                <Swiper.Slide>
                    <img src={event.image} alt="" />
                </Swiper.Slide>
            )}
        </Swiper>
    )
}

export default BannerSlide