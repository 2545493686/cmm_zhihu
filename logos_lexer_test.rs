// 依赖 logos = "0.12.0"

use logos::{Logos};

#[derive(Debug, PartialEq, Logos)]
pub enum TokenType {
    // Logos requires one token variant to handle errors,
    // it can be named anything you wish.
    #[error]
    Error,
    #[token("if")]
    If,
    #[token("return")]
    Return,
    #[token("(")]
    LeftParen,
    #[token(")")]
    RightParen,
    #[token("{")]
    LeftOpenBrace,
    #[token("}")]
    RightOpenBrace,
    #[token("=")]
    AssignmentOperator,
    #[token("-")]
    Minus,
    #[token("+")]
    Plus,
    #[token("*")]
    Multiply,
    #[token("/")]
    Divide,
    #[token("<")]
    LeftAngularBracket,
    #[token(">")]
    RightAngularBracket,
    #[token(";")]
    EOL,
    #[regex(r"[A-Z_a-z][A-Z_a-z0-9]*")]
    Identifier,
    #[regex(r"0|[1-9][0-9]*")]
    Integer,
    #[token(" ")]
    Space,
}

fn main() {
    let source = 
    "int abs(int value)\r\n{\r\nif (value < 0)\r\n{\r\nreturn -value;\r\n}\r\nreturn value;\r\n}\r\n\r\nint i = 114;\r\nprint(abs(i - 514));"
    .repeat(100000);

    let mut lexer = TokenType::lexer(&source);
    let mut tokens = Vec::new();

    // 计时开始
    let start = std::time::Instant::now();
    
    while let Some(token) = lexer.next() {
        // 如果不是空格，就加入 tokens
        if token != TokenType::Space && token != TokenType::Error {
            tokens.push(token);
        }
    }

    // 输出时间
    let duration = start.elapsed();
    println!("elapsed: {:?}", duration);

    // 输出长度
    println!("count: {}", tokens.len());

}
