#!/bin/sh
echo -ne '\033c\033]0;Assignment 0\a'
base_path="$(dirname "$(realpath "$0")")"
"$base_path/run.x86_64" "$@"
