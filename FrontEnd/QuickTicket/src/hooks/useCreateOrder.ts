import axios from "axios";
import CryptoJS from "crypto-js";
import moment from "moment";

const config = {
  app_id: "2553",
  key1: "PcY4iZIKFCIdgZvA6ueMcMHHUbRLYjPL",
  endpoint: "https://sb-openapi.zalopay.vn/v2/create",
};

export async function createZaloPayOrder(orderData: {
  fullName: string;
  email: string;
  phone: string;
  quantity: number;
  amount: number;
  description: string;
}) {
  const transID = Math.floor(Math.random() * 1000000);

  // 👇 Khai báo type rõ ràng, bao gồm `mac`
  const order: {
    app_id: string;
    app_trans_id: string;
    app_user: string;
    app_time: number;
    item: string;
    embed_data: string;
    amount: number;
    description: string;
    bank_code?: string;
    mac?: string; // 👈 Thêm mac vào type
  } = {
    app_id: config.app_id,
    app_trans_id: `${moment().format('YYMMDD')}_${transID}`,
    app_user: orderData.email,
    app_time: Date.now(),
    item: JSON.stringify([{ ...orderData }]),
    embed_data: JSON.stringify({}),
    amount: orderData.amount,
    description: orderData.description,
    bank_code: "",
  };

  const data = `${order.app_id}|${order.app_trans_id}|${order.app_user}|${order.amount}|${order.app_time}|${order.embed_data}|${order.item}`;
  order.mac = CryptoJS.HmacSHA256(data, config.key1).toString();

  const response = await axios.post(config.endpoint, null, { params: order });
  return response.data;
}
