// import { Route, Routes } from "react-router-dom";

// //User Pages
// import Register from "./components/Register";
// import Terms from "./components/Terms";
// import Login from "./components/Login";
// import Forget from "./components/Forget";
// import Home from "./components/Home";
// import About from "./components/About";
// import Navbar from "./components/Navbar";
// import Insert from "./components/Insert";
// import Update from "./components/Update";
// import View from "./components/View";
// import Donations from "./components/Donations";
// import Donationcard from "./components/Donationcard";
// import Profile from "./components/Profile";
// import Footer from "./components/Footer";

// //Admin pages
// import Adminnav from "./components/Adminnav";
// import Adminreq from "./components/Adminreq";
// import Adminview from "./components/Adminview";
// import Adminusers from "./components/Adminusers";

// function App() {
//   return (
//     <div>
//       <Routes>
//       {/* User Routes */}
//       <Route exact path="/" component={Login} />
//       <Route exact path="/forget">
//         <Forget />
//       </Route>
//       <Route exact path="/register" component={Register} />
//       <Route exact path="/home">
//         <Navbar />
//         <Home />
//         <Footer />
//       </Route>
//       <Route exact path="/aboutus">
//         <Navbar />
//         <About />
//         <Footer />
//       </Route>
//       <Route exact path="/termsandconditions">
//         <Terms />
//       </Route>
//       <Route exact path="/donate">
//         <Navbar />
//         <Insert />
//         <Footer />
//       </Route>
//       <Route exact path="/update">
//         <Navbar />
//         <Update />
//         <Footer />
//       </Route>
//       <Route exact path="/view">
//         <Navbar />
//         <View />
//         <Footer />
//       </Route>
//       <Route exact path="/donations">
//         <Navbar />
//         <Donations />
//         <Footer />
//       </Route>
//       <Route exact path="/card">
//         <Donationcard />
//       </Route>
//       <Route exact path="/profile">
//         <Navbar />
//         <Profile />
//         <Footer />
//       </Route>

//       {/* Admin routes */}
//       <Route exact path="/admin">
//         <Adminnav />
//         <Adminreq />
//       </Route>
//       <Route exact path="/adminview">
//         <Adminnav />
//         <Adminview />
//       </Route>
//       <Route exact path="/adminusers">
//         <Adminnav />
//         <Adminusers />
//       </Route>
//       </Routes>
//     </div>
//   );
// }

// export default App;

import { Route, Routes } from "react-router-dom";

// User Pages
import Register from "./components/Register";
import Terms from "./components/Terms";
import Login from "./components/Login";
import Forget from "./components/Forget";
import Home from "./components/Home";
import About from "./components/About";
import Navbar from "./components/Navbar";
import Insert from "./components/Insert";
import Update from "./components/Update";
import View from "./components/View";
import Donations from "./components/Donations";
import Donationcard from "./components/Donationcard";
import Profile from "./components/Profile";
import Footer from "./components/Footer";

// Admin pages
import Adminnav from "./components/Adminnav";
import Adminreq from "./components/Adminreq";
import Adminview from "./components/Adminview";
import Adminusers from "./components/Adminusers";
import Contact from "./components/Contact";
import Payment from "./components/Payment";
import AdminCreateUser from "./components/AdminCreateUser";


function App() {
  return (
    <div>
      <Routes>
        {/* User Routes */}
        <Route exact path="/" element={<Login />} />
        <Route exact path="/forget" element={<Forget />} />
        <Route exact path="/register" element={<Register />} />
        <Route
          exact
          path="/home"
          element={
            <>
              <Navbar />
              <Home />
              <Footer />
            </>
          }
        />
        <Route
          exact
          path="/aboutus"
          element={
            <>
              <Navbar />
              <About />
              <Footer />
            </>
          }
        />
        <Route
          exact
          path="/contactus"
          element={
            <>
              <Navbar />
              <Contact />
              <Footer />
            </>
          }
        />
        <Route exact path="/termsandconditions" element={<Terms />} />
        <Route
          exact
          path="/donate"
          element={
            <>
              <Navbar />
              <Insert />
              <Footer />
            </>
          }
        />
        <Route
          exact
          path="/update"
          element={
            <>
              <Navbar />
              <Update />
              <Footer />
            </>
          }
        />
        <Route
          exact
          path="/view"
          element={
            <>
              <Navbar />
              <View />
              <Footer />
            </>
          }
        />
        <Route
          exact
          path="/donations"
          element={
            <>
              <Navbar />
              <Donations />
              <Footer />
            </>
          }
        />
        <Route exact path="/card" element={<Donationcard />} />
        <Route
          exact
          path="/profile"
          element={
            <>
              <Navbar />
              <Profile />
              <Footer />
            </>
          }
        />

        <Route
          exact
          path="/payment"
          element={
            <>
              <Navbar />
              <Payment />
              <Footer />
            </>
          }
        />

        {/* Admin Routes */}
        <Route
          exact
          path="/admin"
          element={
            <>
              <Adminnav />
              <Adminreq />
            </>
          }
        />
        <Route
          exact
          path="/adminview"
          element={
            <>
              <Adminnav />
              <Adminview />
            </>
          }
        />
        <Route
          exact
          path="/adminusers"
          element={
            <>
              <Adminnav />
              <Adminusers />
            </>
          }
        />
        <Route
          exact
          path="/createuser"
          element={
            <>
              <Adminnav />
              <AdminCreateUser/>
            </>
          }
        />
      </Routes>
    </div>
  );
}

export default App;
