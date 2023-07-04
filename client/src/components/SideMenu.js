import { Menu } from "antd";
import {
  AppstoreOutlined,
  ShopOutlined,
  ShoppingCartOutlined,
  UserOutlined,
} from "@ant-design/icons";
import { useState, useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";
function SideMenu() {
  //使用 useLocation hook 取得目前的網址路徑。
  const location = useLocation();
  //使用 useState hook 創建一個名為 selectedKeys 的狀態變數，並將初始值設置為 /。
  const [selectedKeys, setSelectedKeys] = useState("/");
  //使用 useEffect hook 監聽 location.pathname 的變化，一旦 location.pathname 發生變化，就更新 selectedKeys 的值為新的路徑。
  useEffect(() => {
    const pathName = location.pathname;
    setSelectedKeys(pathName);
  }, [location.pathname]);
  //使用 useNavigate hook 創建一個名為 navigate 的函式，可以用於導航到不同的網址路徑。
  const navigate = useNavigate();
  return (
    <div className="SideMenu">
      <Menu
        className="SideMenuVertical"
        mode="vertical"
        onClick={(item) => {
          //item.key
          navigate(item.key);
        }}
        items={[
          // {
          //   label: "Dashboard",
          //   icon: <AppstoreOutlined />,
          //   key: "/",
          // },
          // {
          //   label: "Inventory",
          //   icon: <ShopOutlined />,
          //   key: "/inventory",
          // },
          {
            label: "Orders",
            icon: <ShoppingCartOutlined />,
            key: "/orders",
          },
          {
            label: "Customers",
            icon: <UserOutlined />,
            key: "/customers",
          },
        ]}
      ></Menu>
    </div>
  );
}
export default SideMenu;
