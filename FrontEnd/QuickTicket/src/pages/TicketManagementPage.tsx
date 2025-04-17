import TicketCard from '@/components/shared/TicketCard';
import { useGetTickets, Ticket } from '@/hooks/useGetTickets';
import React from 'react';
import { Header, Page, Tabs } from 'zmp-ui';

const TicketList = ({ tickets }: { tickets: Ticket[] }) => {
  return (
    <div className="p-4 space-y-4">
      {tickets.map((ticket) => (
       <TicketCard key={ticket.id} ticket={ticket} />
      ))}
    </div>
  );
};

const TicketManagementPage = () => {
  const { upcomingTickets, endedTickets } = useGetTickets();

  return (
    <Page className="py-[60px]">
      <Header title="Quản lý thông tin vé" />
      <Tabs className="mt-6">
        <Tabs.Tab label="Sắp diễn ra" key="upcoming">
          <TicketList tickets={upcomingTickets} />
        </Tabs.Tab>
        <Tabs.Tab label="Đã kết thúc" key="ended">
          <TicketList tickets={endedTickets} />
        </Tabs.Tab>
      </Tabs>
    </Page>
  );
};

export default TicketManagementPage;
