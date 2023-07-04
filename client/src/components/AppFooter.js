import Typography from "antd/es/typography/Typography";

function AppFooter() {
  return (
    <div className="AppFooter">
      <Typography.Link href="tel:123-456-7890">123-456-7890</Typography.Link>
      <Typography.Link
        href="https://www.google.com.tw/?hl=zh_TW"
        target="_blank"
      >
        Google
      </Typography.Link>
      <Typography.Link>Term of Use</Typography.Link>
    </div>
  );
}

export default AppFooter;
