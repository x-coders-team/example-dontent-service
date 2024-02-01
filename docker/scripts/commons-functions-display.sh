#!/bin/bash

function print_messange_log() {
	type_message="$1";
	content_message="$2";
	font_color_red_normal='\033[0;31m'
	font_color_green_normal='\033[0;32m'
	background_color_red='\033[41m'
	background_color_green='\033[42m'
	clear_font='\033[0m';

	case "${type_message}" in
		"ok") echo -e ${background_color_green}[OK]${clear_font} ${font_color_green_normal}${content_message}${clear_font} ;;
		"error") echo -e ${background_color_red}[ERROR]${clear_font} ${font_color_red_normal}${content_message}${clear_font} ;;
		*) echo -e "${content_message}"
	esac
}
