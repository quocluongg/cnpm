import { Button, Page } from "zmp-ui";
import BotNavigation from "@/components/shared/BotNavigation";
import { useState } from "react";
import HomePage from "./HomePage";
import LoginPage from "./LoginPage";
import TicketManagementPage from "./TicketManagementPage";
import { Link } from 'react-router-dom'
import EventManagerPage from "./EventManagerPage";

function Home() {
  //active tab for bottom navigation state
  const [activeTab, setActiveTab] = useState("home");

  return (
    <Page
      className="flex flex-col justify-center space-y-6 bg-cover bg-center bg-no-repeat bg-white dark:bg-black"
    >
      {activeTab === 'home' && (<HomePage/>)}
      {activeTab === 'ticket' && (<TicketManagementPage/>)}
      {activeTab === 'profile' && (<LoginPage/>)}
      {/* <EventManagerPage></EventManagerPage> */}
      <BotNavigation activeKey={activeTab} onChange={(key) => setActiveTab(key)}></BotNavigation>
    </Page>
  );
}

export default Home;
