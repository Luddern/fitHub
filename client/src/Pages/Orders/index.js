import {
  Avatar,
  Space,
  Table,
  Typography,
  Button,
  Modal,
  Form,
  Input,
} from "antd";
import { useEffect, useState } from "react";
import { getOrderList, addOrder } from "../../api";
import { DatePicker, Select } from "antd";
import { PlusOutlined, DeleteOutlined, EditOutlined } from "@ant-design/icons";

function Orders() {
  const [isEditing, setIsEditing] = useState(false);
  const [loading, setLoading] = useState(false);
  const [dataSource, setDataSource] = useState([]);
  const [editingOrderId, setEditingOrderId] = useState(null);
  const [editingVisible, setEditingVisible] = useState(false);
  const [addingVisible, setAddingVisible] = useState(false);
  const [deletingOrderId, setDeletingOrderId] = useState(null);
  const [deletingVisible, setDeletingVisible] = useState(false);
  const [statusOptions, setStatusOptions] = useState([
    { label: "待處理", value: 0 },
    { label: "處理中", value: 1 },
    { label: "已完成", value: 2 },
    { label: "已取消", value: 3 },
  ]);
  const [tableParams, setTableParams] = useState({
    pagination: {
      current: 1,
      pageSize: 10,
    },
  });

  const [form] = Form.useForm();

  useEffect(() => {
    setLoading(true);
    initTable();
  }, []);

  async function initTable(page = 1, pageSize = 30) {
    const res = await getOrderList(page, pageSize);
    setDataSource(res.data.items);
    setTableParams({
      pagination: {
        current: page,
        pageSize: pageSize,
        total: res.data.total,
      },
    });
    setLoading(false);
  }

  //當編輯按鈕被點擊時，設定要編輯的訂單ID，找到要編輯的資料並填充表單資料，設定編輯狀態為真並顯示編輯視窗。
  const handleEditClick = (orderId) => {
    setEditingOrderId(orderId);
    // 找到要編輯的資料
    const editingData = dataSource.find((item) => item.orderId === orderId);
    // 填充表單資料
    form.setFieldsValue(editingData);
    setIsEditing(true);
    setEditingVisible(true);
  };

  //當新增按鈕被點擊時，重置表單字段，設定編輯狀態為假並顯示新增視窗。
  const handleAddClick = () => {
    form.resetFields(); // 重置表單字段
    setIsEditing(false);
    setAddingVisible(true);
  };

  //當刪除按鈕被點擊時，設定要刪除的訂單ID並顯示刪除視窗。
  const handleDeleteClick = (orderId) => {
    setDeletingOrderId(orderId);
    setDeletingVisible(true);
  };

  //當取消按鈕被點擊時，隱藏編輯視窗和新增視窗。
  const handleCancel = () => {
    setEditingVisible(false);
    setAddingVisible(false);
  };

  //當刪除取消按鈕被點擊時，隱藏刪除視窗。
  const handleDeleteCancel = () => {
    setDeletingVisible(false);
  };

  //當確定按鈕被點擊時，根據編輯狀態更新資料源。如果是編輯狀態，則更新指定訂單ID的資料；如果是新增狀態
  //則新增一筆包含隨機訂單ID和表單值的資料。最後，隱藏編輯視窗或新增視窗
  const handleOk = (values) => {
    if (isEditing) {
      // 編輯數據
      setDataSource((prevDataSource) =>
        prevDataSource.map((item) => {
          if (item.orderId === editingOrderId) {
            return {
              ...item,
              ...values,
            };
          }
          return item;
        })
      );
      setEditingVisible(false);
    } else {
      // 新增數據

      const newData = {
        orderId: Math.random(),
        ...values,
      };
      // 設置 orderDate 為現在時間
      newData.orderDate = new Date().toISOString();
      console.log(newData);
      newData.totalAmount = parseInt(newData.totalAmount);
      newData.status = parseInt(newData.status);

      addOrder(newData)
        .then(async (res) => {
          await initTable();
          setAddingVisible(false);
        })
        .catch((err) => {
          console.log(err);
        });
    }
  };

  const handleDeleteOk = () => {
    setDataSource((prevDataSource) =>
      prevDataSource.filter((item) => item.orderId !== deletingOrderId)
    );
    setDeletingVisible(false);
  };

  const handleTableChange = (pagination, filters, sorter) => {
    initTable(pagination.current, pagination.pageSize);
    if (pagination.pageSize !== tableParams.pagination.pageSize) {
      setDataSource([]);
    }
  };

  return (
    <Space size={20} direction="vertical">
      <Typography.Title level={4}>訂單</Typography.Title>
      <Button type="primary" icon={<PlusOutlined />} onClick={handleAddClick}>
        新增
      </Button>
      <Table
        loading={loading}
        columns={[
          {
            title: "客戶ID",
            dataIndex: "customerId",
          },
          {
            title: "總金額",
            dataIndex: "totalAmount",
          },
          {
            title: "訂單狀態",
            dataIndex: "status",
            render: (status) => (
              <span>
                {statusOptions.find((item) => item.value === status).label}
              </span>
            ),
          },
          {
            title: "訂單日期",
            dataIndex: "orderDate",
          },
          {
            title: "業務名稱",
            dataIndex: "salesName",
          },

          {
            title: "操作",
            dataIndex: "orderId",
            render: (orderId) => (
              <div style={{ display: "flex", alignItems: "center", gap: 10 }}>
                <Button type="primary" onClick={() => handleEditClick(orderId)}>
                  編輯
                </Button>
                <Button danger onClick={() => handleDeleteClick(orderId)}>
                  刪除
                </Button>
              </div>
            ),
          },
        ]}
        dataSource={dataSource}
        pagination={tableParams.pagination}
        onChange={handleTableChange}
      ></Table>

      <Modal
        title="編輯訂單"
        visible={editingVisible}
        onCancel={handleCancel}
        footer={null}
      >
        <Form onFinish={handleOk} form={form}>
          <Form.Item
            label="客戶ID"
            name="customerId"
            rules={[{ required: true, message: "請輸入客戶ID！" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="總金額"
            name="totalAmount"
            rules={[{ required: true, message: "請輸入總金額！" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="訂單狀態"
            name="status"
            rules={[{ required: true, message: "請選擇訂單狀態！" }]}
          >
            <Select options={statusOptions}></Select>
          </Form.Item>
          <Form.Item
            label="訂單日期"
            name="orderDate"
            rules={[{ required: true, message: "請輸入訂單日期" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="業務名稱"
            name="salesName"
            rules={[{ required: true, message: "請輸入業務名稱！" }]}
          >
            <Input />
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit">
              確定
            </Button>
          </Form.Item>
        </Form>
      </Modal>

      <Modal
        title="新增訂單"
        visible={addingVisible}
        onCancel={handleCancel}
        footer={null}
      >
        <Form onFinish={handleOk}>
          <Form.Item
            label="客戶ID"
            name="customerId"
            rules={[{ required: true, message: "請輸入客戶ID！" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="總金額"
            name="totalAmount"
            rules={[{ required: true, message: "請輸入總金額！" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="訂單狀態"
            name="status"
            rules={[{ required: true, message: "請選擇訂單狀態！" }]}
          >
            <Select options={statusOptions}></Select>
          </Form.Item>
          <Form.Item
            label="訂單日期"
            name="orderDate"
            rules={[{ required: true, message: "請輸入訂單日期" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="業務名稱"
            name="salesName"
            rules={[{ required: true, message: "請輸入業務名稱！" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit">
              確定
            </Button>
          </Form.Item>
        </Form>
      </Modal>

      <Modal
        title="刪除訂單"
        visible={deletingVisible}
        onCancel={handleDeleteCancel}
        onOk={handleDeleteOk}
        okText="確認"
        cancelText="取消"
        closable={false}
      >
        <p>確定刪除該訂單？</p>
      </Modal>
    </Space>
  );
}

export default Orders;
