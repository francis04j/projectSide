# SAST - static application security testing
# This script will remove non functional code files

#!/usr/bin/env bash
set -o errexit
set -o errtrace
set -o nounset# TODO: Replace with "$(mktemp -d)"
TEMP_DIR="./veracode/"
OUTPUT_FILE="demoApp.zip"rm -rf "${TEMP_DIR}"git clone --depth=1 github.com/projectSide.git "${TEMP_DIR}"cd "${TEMP_DIR}"yarn install# Git
rm -rf ./.git/;
find . -type f -name .gitignore -delete;# Delete packages
find packages/ -type d -depth 2 -not \
    \( \
         -wholename '*/wfe-origination-portal' \
    \) -prune -exec rm -rf {} \;# NPM
find node_modules/ -type d -maxdepth 2 -not \
    \( \
        -wholename "node_modules/" -o \
        -wholename "*/@cob-lvs" -o \
        -wholename "*/@cob-lvs/env-lib" -o \
        -wholename "*/@cob-lvs/security-lib" -o \
        -wholename "*/@cob-lvs/server-error-lib" -o \
        -wholename "*/@cob-lvs/server-context-lib" \
        -wholename "*/@cob-lvs/jwt-session-lib" \
     \) -prune -exec rm -rf {} \;# Delete nested node modules
find node_modules/* -type d -iname node_modules -prune -exec rm -rf {} \;find . -type f -name .npmrc -delete;
#find . -type f -name .nvmrc -delete;# Yarn
find . -type d -name .yarn -prune -exec rm -rf {} \;
find . -type f -name .yarnrc -delete;
find . -type f -name .yarn-integrity -delete;
# HTML
find . -type f -name '*.html' -delete;# Documentation
find . -type f -name '*.md' -delete;
find . -type f -name 'LICENSE' -delete;# Config
find . -type f -name '*.json' -not -name 'package.json' -delete ;
find . -type f -name '*.yml' -not -name '.yarnrc.yml' -delete;
find . -type f -name '*.yaml' -delete;# CSS
find . -type f -name '*.css' -delete;
find . -type f -name '*.scss' -delete;# Documents
find . -type f -name '*.docx' -delete;# Fonts
find . -type d -name fonts -prune -exec rm -rf {} \;
find . -type f -name '*.ttf' -delete;
find . -type f -name '*.woff' -delete;
find . -type f -name '*.woff2' -delete;# Image Assets
find . -type f -name '*.png' -delete;
find . -type f -name '*.jpg' -delete;
find . -type f -name '*.jpeg' -delete;
find . -type f -name '*.svg' -delete;# Protobuffers
find . -type f -name '*.proto' -delete;
# Other (PEG Grammar)
find . -type f -name '*.peg' -delete;# Lerna
find . -type f -name lerna.json -delete;# Tests
rm -rf packages/tests;
find . -type f -name jest.config.js -delete;
find . -type f -name '*.spec.*' -delete;
find . -type d -name jest -prune -exec rm -rf {} \;
find . -type d -name __snapshots__ -prune -exec rm -rf {} \;
find . -type d -name bdd -prune -exec rm -rf {} \;
find . -type d -name reports -prune -exec rm -rf {} \;
find . -type d -name test -prune -exec rm -rf {} \;
find . -type d -name tests -prune -exec rm -rf {} \;
find . -type d -name test-utils -prune -exec rm -rf {} \;
find . -type d -name testMocks -prune -exec rm -rf {} \;# Babel
find . -type f -name babel.config.js -delete;
find . -type f -name .babelrc -delete;# Webpack
find . -type f -name 'webpack.*.js' -delete;
find . -type d -name webpack -prune -exec rm -rf {} \;# DevOps
find . -type f -name .env -delete;
find . -type f -name Dockerfile -delete;
find . -type f -name docker-compose.yml -delete;
find . -type f -name .dockerignore -delete;
find . -type d -name pm2 -prune -exec rm -rf {} \;
find . -type d -name jenkins -prune -exec rm -rf {} \;
find . -type d -name deployment -prune -exec rm -rf {} \;
find . -type d -name scripts -prune -exec rm -rf {} \;# ESlint
find . -type f -name .eslintignore -delete;
find . -type f -name .eslintrc -delete;
# Browserlist
find . -type f -name .browserslistrc -delete;# Stylelint
find . -type f -name .stylelintrc -delete;
find . -type f -name .stylelintignore -delete;# PostCSS
find . -type f -name postcss.config.js -delete;# Prettier
find . -type f -name .prettierignore -delete;# EditorConfig
find . -type f -name .editorconfig -delete;find . -type d -empty -deletecd ../
tar -czvf "${OUTPUT_FILE}" "${TEMP_DIR}"
