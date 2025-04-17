import { getSystemInfo } from "zmp-sdk";
import {
  AnimationRoutes,
  App,
  Route,
  SnackbarProvider,
  ZMPRouter,
} from "zmp-ui";
import { AppProps } from "zmp-ui/app";

import HomePage from "@/pages/index";
import DetailPage from "@/pages/DetailPage";
import SignUpPage from "@/pages/SignUpPage";
import LoginPage from "@/pages/LoginPage";
import TicketBookingPage from "@/pages/TicketBookingPage";
import AddEventPage from "@/pages/AddEventPage";

const Layout = () => (
  <App theme={getSystemInfo().zaloTheme as AppProps["theme"]}>
    <SnackbarProvider>
      <ZMPRouter>
        <AnimationRoutes>
          <Route path="/" element={<HomePage />}></Route>
          <Route path="/events/:id" element={<DetailPage />} />
          <Route path="/signup" element={<SignUpPage/>}/>
          <Route path="/login" element={<LoginPage/>}/>
          <Route path="/booking" element={<TicketBookingPage/>}/>
          <Route path="/add" element={<AddEventPage />} />
        </AnimationRoutes>
      </ZMPRouter>
    </SnackbarProvider>
  </App>
);
export default Layout;
