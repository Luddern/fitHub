import axios from "axios";

const api = axios.create({
  baseURL: "/api",
});

// 添加新客戶
export const addCustomer = async (customerData) => {
  return api.post("/Customer", customerData);
};

// 更新客戶資料
export const updateCustomer = async (customerData) => {
  return api.put("/Customer", customerData);
};

// 刪除客戶
export const deleteCustomer = async (customerId) => {
  return api.delete(`/Customer/${customerId}`);
};

// 獲取特定客戶的信息
export const getCustomer = async (customerId) => {
  return api.get(`/Customer/${customerId}`);
};

// 獲取客戶列表
export const getCustomerList = async (pageNo, pageSize) => {
  return api.get("/Customer/list", {
    params: { pageNo, pageSize },
  });
};

// 以篩選條件獲取客戶列表
export const filterCustomers = async (filters) => {
  return api.get("/Customer/filter", { params: filters });
};
