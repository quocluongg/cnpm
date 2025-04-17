import toast, { Toaster } from "react-hot-toast";
import { Link } from "react-router-dom"; // 👉 import Link
import { Button, Header, Input, Page } from "zmp-ui";

function LoginPage() {
  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const data = new FormData(e.currentTarget);
    const email = data.get("email")?.toString();
    const password = data.get("password")?.toString();

    if (email === "user@example.com" && password === "123456") {
      toast.success("Đăng nhập thành công!", { duration: 1500 });
      // Không navigate nữa, toast xong thì người dùng tự bấm nút hoặc có thể dùng <Link>
    } else {
      toast.error("Email hoặc mật khẩu không đúng!");
    }
  };

  return (
    <Page className="py-[60px]">
      <Toaster position="top-center" reverseOrder={false} />
      <Header title="Đăng nhập tài khoản" />
      <form className="h-full flex flex-col justify-between" onSubmit={handleSubmit}>
        <div className="py-2 space-y-2">
          <div className="bg-section p-4 grid gap-4">
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
          </div>
          <div className="px-4 text-sm text-center text-gray-500">
            Chưa có tài khoản?{" "}
            <Link to="/signup" className="text-primary font-medium underline">
              Đăng ký
            </Link>
          </div>
        </div>
        <div className="p-6 pt-4 bg-section">
          <Button htmlType="submit" fullWidth>
            Đăng nhập
          </Button>
        </div>
      </form>
    </Page>
  );
}

export default LoginPage;
