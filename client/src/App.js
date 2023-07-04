import { Space } from "antd";
import AppHeader from "./components/AppHeader";
import AppFooter from "./components/AppFooter";
import SideMenu from "./components/SideMenu";
import PageContent from "./components/PageContent";
// import React from "react";
import "./App.css";
import CustomerList from "./components/CustomerList";
function App() {
  // return (
  //   <div className="App">
  //     <header className="App-header">
  //       <img src={logo} className="App-logo" alt="logo" />
  //       <p>
  //         Edit <code>src/App.js</code> and save to reload.
  //       </p>
  //       <a
  //         className="App-link"
  //         href="https://reactjs.org"
  //         target="_blank"
  //         rel="noopener noreferrer"
  //       >
  //         Learn React
  //       </a>
  //     </header>
  //   </div>
  // );
  return (
    <div className="App">
      <AppHeader />
      <Space className="SideMenuAndPageContent">
        <SideMenu />
        <PageContent />
      </Space>
      <AppFooter />
      <CustomerList />
    </div>
  );
}

export default App;
