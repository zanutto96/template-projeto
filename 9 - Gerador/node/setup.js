#!/usr/bin/env node

console.log('🚀 Setting up Code Generator MCP Server...\n');

const { execSync } = require('child_process');
const path = require('path');

try {
  console.log('📦 Installing dependencies...');
  execSync('npm install', { stdio: 'inherit' });
  
  console.log('\n🔨 Building project...');
  execSync('npm run build', { stdio: 'inherit' });
  
  console.log('\n✅ Setup complete!');
  console.log('\n📋 Next steps:');
  console.log('1. Add the MCP server to your VS Code settings:');
  console.log('   Copy the configuration from mcp-config.json to your VS Code settings');
  console.log('2. Restart VS Code');
  console.log('3. Test with: @code-generator validate connection string "your-connection-string"');
  console.log('\n🎯 Ready to generate code from your databases!');
  
} catch (error) {
  console.error('❌ Setup failed:', error.message);
  process.exit(1);
}