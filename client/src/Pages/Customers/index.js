import { Avatar, Space, Table, Typography } from "antd";
import { useEffect, useState } from "react";
import { getCustomerList } from "../../api";

function Customers() {
  const [loading, setLoading] = useState(false);
  const [dataSource, setDataSource] = useState([]);
  // const [tableParams, setTableParams] = useState({
  //   pagination: {
  //     current: 1,
  //     pageSize: 30,
  //   },
  // });

  useEffect(() => {
    setLoading(true);
    getCustomerList(1, 90).then((res) => {
      setDataSource(res.data.items);
      setLoading(false);
      console.log(res);
    });
  }, []);

  return (
    <Space size={20} direction="vertical">
      <Typography.Title level={4}>Customers</Typography.Title>
      <Table
        loading={loading}
        columns={[
          {
            title: "CustID",
            dataIndex: "customerId",
            fixed: "left", // 添加固定列属性
          },
          {
            title: "Name",
            dataIndex: "name",
            fixed: "left",
          },
          {
            title: "Country",
            dataIndex: "country",
          },
          {
            title: "State",
            dataIndex: "state",
          },
          {
            title: "Zip",
            dataIndex: "zip",
          },

          {
            title: "City",
            dataIndex: "city",
          },

          {
            title: "address",
            dataIndex: "address",
            // render: (address) => {
            //   return (
            //     <span>
            //       {address.address}, {address.city}
            //     </span>
            //   );
            // },
          },
        ]}
        dataSource={dataSource}
        pagination={{
          pageSize: 30,
          pageSizeOptions: [], //移除選擇要印出來的數量
        }}
        scroll={{ x: "max-content" }} // 这里添加了滚动属性，以便在需要时可以滚动查看其他列
      ></Table>
    </Space>
  );
}
export default Customers;
