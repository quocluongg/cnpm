import toast, { Toaster } from "react-hot-toast";
import { Link, useNavigate } from "react-router-dom";
import { Button, Header, Icon, Input, Page } from "zmp-ui";

function SignUpPage() {
  const navigate = useNavigate();

  return (
  <Page className='pt-[60px]'>
    <div><Toaster/></div>
    <Header title='Đăng kí tài khoản'></Header>
    <Toaster
      position="top-center"
      reverseOrder={false}
    />
    <form
      className="h-full flex flex-col justify-between"
      onSubmit={(e) => {
        e.preventDefault();
        const data = new FormData(e.currentTarget);
        const newUser = {};
        data.forEach((value, key) => {
          newUser[key] = value;
        });

        // Fake API Call or use your actual registration logic here
        console.log("Đăng ký với thông tin:", newUser);
        toast.success("Đăng ký thành công!",{
          duration: 1500, // 1.5s
        });
        // navigate("/"); // Điều hướng sau đăng ký
      }}
    >
      <div className="py-2 space-y-2 bg-section">
        <div className="bg-section p-4 grid gap-4">
          <Input
            name="name"
            label="Họ và tên"
            placeholder="Nhập họ và tên"
            required
          />
          <Input
            name="email"
            type="text"
            label="Email"
            placeholder="abc@example.com"
            required
          />
          <Input
            name="password"
            type="password"
            label="Mật khẩu"
            placeholder="Nhập mật khẩu"
            required
          />
          <Input
            name="phone"
            label="Số điện thoại"
            placeholder="0912345678"
            required
          />
          <Input
            name="address"
            label="Địa chỉ"
            placeholder="Nhập địa chỉ"
            required
          />
        </div>
      </div>
      <div className="px-4 text-sm text-center text-gray-500">
          Đã có tài khoản?{" "}
          <Link to="/login" className="text-primary font-medium underline">
            Đăng nhập
          </Link>
        </div>
      <div className="p-6 pt-4 bg-section">
        <Button htmlType="submit" fullWidth>
          Đăng ký
        </Button>
      </div>
    </form>
  </Page>    
  );
}

export default SignUpPage;
