import React, { useEffect, useState } from "react";
import { getCustomerList } from "../api/customers";

const CustomerList = () => {
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    const fetchCustomers = async () => {
      try {
        const response = await getCustomerList(1, 10);
        setCustomers(response.data.items);
      } catch (error) {
        console.error("Failed to fetch customers:", error);
      }
    };
    fetchCustomers();
  }, []);

  return (
    <div>
      {/* <h2>Customers</h2> */}
      <ul>
        {/* {customers.map((customer) => (
                    <li key={customer.customerId}>{customer.name}</li>
                ))} */}
        {customers.forEach((element) => {
          <li key={element.customerId}>{element.name}</li>;
        })}
      </ul>
    </div>
  );
};

export default CustomerList;
