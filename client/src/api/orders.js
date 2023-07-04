import axios from "axios";
const api = axios.create({
  baseURL: "/api",
});
// 添加新訂單
export const addOrder = async (orderData) => {
  return api.post("/Order", orderData);
};

// 更新訂單資料
export const updateOrder = async (orderData) => {
  return api.put("/Order", orderData);
};

// 刪除訂單
export const deleteOrder = async (orderId) => {
  return api.delete(`/Order/${orderId}`);
};

// 獲取特定訂單的信息
export const getOrder = async (orderId) => {
  return api.get(`/Order/${orderId}`);
};

// 獲取訂單列表
export const getOrderList = async (pageNo, pageSize) => {
  return api.get("/Order/list", {
    params: { pageNo, pageSize },
  });
};

// 以篩選條件獲取訂單列表
export const filterOrders = async (filters) => {
  return api.get("/Order/filter", { params: filters });
};
