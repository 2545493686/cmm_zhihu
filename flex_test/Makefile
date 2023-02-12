BUILD_DIR = ./build
BUILD_SRC_DIR = $(BUILD_DIR)/src

run: 
	@mkdir -p $(BUILD_SRC_DIR)
	@flex --outfile $(BUILD_SRC_DIR)/lex.yy.c flex_test.l
	@gcc $(BUILD_SRC_DIR)/lex.yy.c -o $(BUILD_DIR)/flex_test
	@python3 input_generater.py >> $(BUILD_DIR)/input.txt
	@$(BUILD_DIR)/flex_test < $(BUILD_DIR)/input.txt

clean:
	@rm -rf ./build