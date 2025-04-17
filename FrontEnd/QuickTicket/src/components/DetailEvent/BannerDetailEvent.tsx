import { useGetEvents } from '@/hooks'
import React from 'react'
import { Box, Button, useParams } from 'zmp-ui';

const BannerDetailEvent = ({eventId}) => {
    const { getEventById, loading } = useGetEvents()
    const event = getEventById(eventId || '')

    if (loading) return <p>Đang tải chi tiết...</p>
    if (!event) return <p>Không tìm thấy sự kiện</p>

    return (
        <Box mt={1}>
            <Button size="large">Button</Button>
        </Box>
    )
}

export default BannerDetailEvent